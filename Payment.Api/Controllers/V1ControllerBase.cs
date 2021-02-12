using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class V1ControllerBase : ControllerBase
    {
        internal protected readonly IMediator _mediator;
        public V1ControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
