using Microsoft.AspNetCore.Mvc;
using WebHooks.DTO;
using WebHooks.Mappers;
using WebHooks.Models;
using WebHooks.Services;

namespace WebHooks.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class WebhookController : ControllerBase{
    private readonly IWebhookService _webhookService;

    public WebhookController(IWebhookService webhookService){
        _webhookService = webhookService;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<WebhookResponseDTO>> CreateWebhook([FromBody] WebhookRequestDTO dto)
    {
        Webhook savedWebhook = await _webhookService.CreateWebhook(dto.MapToWebhookEntity());
        
        return Created("", savedWebhook.MapToWebhookDto());
    }

    [HttpGet]
    [Route("")]
    public async Task<ICollection<WebhookResponseDTO>> GetWebhooks()
    {
        return (await _webhookService.GetWebhooks()).Select(w => w.MapToWebhookDto()).ToList();
    }

    [HttpDelete]
    [Route("{webhookId}")]
    public async Task<ActionResult> DeleteWebhook(long webhookId){
        bool deleted = await _webhookService.DeleteWebhook(webhookId);

        if (!deleted)
            return BadRequest();
        
        return Ok();
    }
}