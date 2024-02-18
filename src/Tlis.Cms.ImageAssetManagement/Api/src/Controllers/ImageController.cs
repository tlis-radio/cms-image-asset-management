using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.ImageAssetManagement.Api.Constants;
using Tlis.Cms.ImageAssetManagement.Api.Controllers.Attributes;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ImageAssetManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ImageController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [Authorize(Policy.ImageRead)]
    [RequestSizeLimit(5000000)]
    [FormFileContentTypeFilter(ContentType = "image/jpeg,image/png")]
    [SwaggerOperation("Get image byt id")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ImageGetByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async ValueTask<ActionResult<ImageGetByIdResponse>> GetById(Guid id)
    {
        var response = await mediator.Send(new ImageGetByIdRequest { Id = id });

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost("user-profile")]
    [Authorize(Policy.ImageWrite)]
    [SwaggerOperation("Save image as user profile image.")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(BaseCreateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async ValueTask<ActionResult<BaseCreateResponse>> CreateUserProfileImage([FromForm] UserProfileImageCreateRequest request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? BadRequest()
            : CreatedAtAction(nameof(GetById), new { response.Id } , response);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy.ImageDelete)]
    [SwaggerOperation("Delete image")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async ValueTask<ActionResult<BaseCreateResponse>> DeleteImage(Guid id)
    {
        var response = await mediator.Send(new ImageDeleteRequest { Id = id });

        return response ? NoContent() : NotFound();
    }
}