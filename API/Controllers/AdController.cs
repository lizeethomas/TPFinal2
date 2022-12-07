using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPFinalStrasbourg.Repositories;
using TPFinalStrasbourg.Services;
using TPFinalStrasbourg.Models;
using TPFinalStrasbourg.DTOs;

namespace TPFinalStrasbourg.Controllers
{
    [Route("api/v1/ads")]
    public class AdController : Controller
    {
        private JWTService _jwtService;
        private AdService _adService;

        public AdController(JWTService jwtService, AdService adService)
        {
            _jwtService = jwtService;
            _adService = adService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult DisplayAllAds()
        {
            List<AdResponseDTO> adResponseDTOs = _adService.DisplayAll();
            if(adResponseDTOs != null){
                return Ok(adResponseDTOs);
            }
            return BadRequest("LeMauvaisCoin ne dispose d'aucune annonce");
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult DisplayAd(int id)
        {
            AdResponseDTO response = _adService.DisplayOneAd(id);
            if(response != null)
            {
                return Ok(response);
            }
            return BadRequest("Aucune annonce trouvée !!!");
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles="admin")]
        public IActionResult DeleteAd(int id)
        {
            if (_adService.Delete(id)) { return Ok("Element supprimé"); }
            return BadRequest("Echec de la suppression");
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult CreateAd([FromBody] AdRequestDTO adRequest)
        {
            AdResponseDTO response = _adService.Create(adRequest);
            return Ok(response);
        }

        [HttpGet("search/{word}")]
        [Authorize]
        public IActionResult FindByKeyword(string word)
        {
            List<AdResponseDTO> adResponses = _adService.SearchByKeyword(word); 
            if (adResponses != null)
            {
                return Ok(adResponses);
            }
            return BadRequest("Aucun résultat trouvé, désolé :'(");
        }

        [HttpPost("{id}/comment")]
        [Authorize]
        public IActionResult AddComment(int id, [FromForm] string comment)
        {
            CommentResponseDTO comResDTO = _adService.AddComment(id, comment);
            if (comResDTO != null)
            {
                return Ok("Commentaire enregistré !");
            }
            return BadRequest("Pas de vilain commentaire aujourd'hui, sorry bro");
        }

        [HttpPost("{id}/picture")]
        [Authorize]
        public IActionResult AddPicture(int id, [FromForm] string url)
        {
            AdResponseDTO response = _adService.AddPicture(id, url);
                        
            return Ok(response);
            
                   
        }
    }
}
