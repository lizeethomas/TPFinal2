using TPFinalStrasbourg.Repositories;
using TPFinalStrasbourg.Services;
using TPFinalStrasbourg.DTOs;
using TPFinalStrasbourg.Models;
using System.Security.Claims;
using Azure;

namespace TPFinalStrasbourg.Services

{
    public class AdService
    {
        private readonly AdRepository _adRepo;
        private readonly UserRepository _userRepo;
        private readonly CategoryRepository _catRepo;
        private readonly IHttpContextAccessor _httpContext;
        private string userRole;
        private string userName;
        private User user;

        public AdService(AdRepository adRepo, UserRepository userRepo, CategoryRepository catRepo, IHttpContextAccessor httpContext)
        {
            _adRepo = adRepo;
            _httpContext = httpContext;
            _userRepo = userRepo;
            _catRepo = catRepo;
        }

        public void setUser()
        {
            this.userRole = _httpContext.HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Role).Value;
            this.userName = _httpContext.HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Name).Value;
            this.user = _userRepo.SearchOne(u => u.Name == this.userName);
        }

        public AdResponseDTO Create(AdRequestDTO req)
        {
            this.setUser();
            AdResponseDTO response = new AdResponseDTO();

            Ad ad = new Ad
            {
                Title = req.Title,
                Description = req.Description,
                Status = this.userRole == "admin" ? "Validée" : "En attente",
                Date = DateTime.Now,
                CategoryId = req.CategoryId,
                User = this.user
            };

            if (_adRepo.Save(ad))
            {
                response.Id = ad.Id;
                response.Description = ad.Description;
                response.Title = ad.Title;
                response.Status = ad.Status;
                response.Category = _catRepo.FindById(req.CategoryId).Name;
                response.Date = ad.Date;

                return response;
            }
            return null;
        }

        public bool Delete(int id)
        {
            Ad ad = _adRepo.FindById(id);
            if (ad != null && _adRepo.Delete(ad))
            {
                return true;
            }
            return false;
        }

        public CommentResponseDTO AddComment(int adId, string text)
        {
            this.setUser();
            Ad ad = _adRepo.FindById(adId);
            Comment comment = new()
            {
                Text = text,
                Status = this.userRole == "admin" ? "Validée" : "En attente",
                Date = DateTime.Now,
                UserId = this.user.Id
            };
            ad.Comments.Add(comment);
            _adRepo.Update();
            CommentResponseDTO commentResponse = new()
            {
                Text = comment.Text,
                Status = comment.Status,
                Date = comment.Date,
                Name = user.Name
            };
            return commentResponse;

        }

        public AdResponseDTO AddPicture(int adId, string url)
        {
            Ad ad = _adRepo.FindById(adId);
            AdResponseDTO response = new AdResponseDTO();
            ad.Images.Add(new Image()
            {
                Url = url,
            });
            if (_adRepo.Update())
            {
                response.Id = ad.Id;
                return response;
            }
            return null;
        }

        public List<AdResponseDTO> DisplayAll()
        {
            List<AdResponseDTO> responses = new List<AdResponseDTO>();

            List<Ad> ads = _adRepo.FindAll();

            foreach(Ad ad in ads)
            {
                List<string> links = new List<string>();
                List< CommentResponseDTO> comments = new List<CommentResponseDTO> ();
                AdResponseDTO response = new AdResponseDTO
                {
                    Id = ad.Id,
                    Title = ad.Title,
                    Description = ad.Description,
                    Date = ad.Date,
                    Status = ad.Status,
                    Category = ad.Category.Name
                };
                ad.Images.ForEach(i=> links.Add(i.Url));
                response.Urls = links;
                ad.Comments.ForEach(c =>
                {
                    CommentResponseDTO comment = new CommentResponseDTO()
                    {
                        Text = c.Text,
                        Date = c.Date,
                        Status = c.Status,
                        Name = _userRepo.FindById(c.UserId).Name
                    };
                    comments.Add(comment);
                });
                    response.Comments = comments;
                responses.Add(response);
            }
            return responses;
        }

        public AdResponseDTO DisplayOneAd(int id)
        {
            List<string> links = new List<string>();
            Ad ad = _adRepo.FindById(id);
            AdResponseDTO adResponse = new()
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                Status = ad.Status,
                Category = ad.Category.Name,
                Date = ad.Date,
            };
            ad.Images.ForEach(i => links.Add(i.Url));
            ad.Comments.ForEach(c =>
            {
                CommentResponseDTO commentResponseDTO = new()
                {
                    Text = c.Text,
                    Date = c.Date,
                    Status = c.Status,
                    Name = _userRepo.FindById(c.UserId).Name
                };
                adResponse.Comments.Add(commentResponseDTO);
            });
            adResponse.Urls = links;
            return adResponse;
        }


        public List<AdResponseDTO> SearchByKeyword(string word)
        {
            List<AdResponseDTO> adResponses = new();
            List<Ad> ads = _adRepo.SearchAll(a => a.Title.Contains(word) || a.Description.Contains(word));
            ads.ForEach(a =>
            {
                List<string> links = new List<string>();
                AdResponseDTO adResponse = new()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Date = a.Date,
                    Status = a.Status,
                    Category = a.Category.Name
                };
                a.Images.ForEach(i => links.Add(i.Url));
                adResponse.Urls = links;
                adResponses.Add(adResponse);
            });
            return adResponses;
        }
    }
}