using Microsoft.AspNetCore.Mvc;
using WebHooks.DTO;
using WebHooks.Mappers;
using WebHooks.Models;
using WebHooks.Services;

namespace WebHooks.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class WebhookController : ControllerBase{
    private readonly IWebhookService _webhookService;

    public WebhookController(IWebhookService webhookService){
        _webhookService = webhookService;
    }

    [Route("webhook")]
    [HttpPost]
    public async Task<ActionResult<WebhookResponseDTO>> CreateWebhook([FromBody] WebhookRequestDTO dto)
    {
        Webhook savedWebhook = await _webhookService.CreateWebhook(dto.MapToWebhookEntity());
        WebhookResponseDTO responseObject = savedWebhook.MapToWebhookDto();
        return Created("", responseObject);
    }

    [HttpGet]
    [Route("webhook")]
    public async Task<ICollection<WebhookResponseDTO>> GetWebhooks()
    {
        return (await _webhookService.GetWebhooks()).Select(w => w.MapToWebhookDto()).ToList();
    }

    [HttpDelete]
    [Route("webhook/{webhookId}")]
    public async Task DeleteWebhook(long webhookId){
        await _webhookService.DeleteWebhook(webhookId);
    }
}