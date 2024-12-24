using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Dtos;
using ToDo.Domain.Entity;

namespace ToDo.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync(cancellationToken);
        return _mapper.Map<List<User>, List<UserDto>>(users);
    }
}