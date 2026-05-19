using CBS.Domain.Models;
using CBS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CBS.Shared.Services;

public class MemberStateService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly List<Member> _members;

    public MemberStateService(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
        using var db = factory.CreateDbContext();
        _members = db.Members.OrderBy(m => m.Id).ToList();
    }

    public IReadOnlyList<Member> Members => _members;

    public void Add(Member member)
    {
        member.Id = 0;
        using var db = _factory.CreateDbContext();
        db.Members.Add(member);
        db.SaveChanges();
        _members.Add(member);
    }

    public void Deactivate(Member member)
    {
        member.IsActive = false;
        Update(member);
    }

    public void Remove(Member member)
    {
        using var db = _factory.CreateDbContext();
        db.Members.Where(m => m.Id == member.Id).ExecuteDelete();
        _members.Remove(member);
    }

    public void Update(Member member)
    {
        using var db = _factory.CreateDbContext();
        db.Members.Update(member);
        db.SaveChanges();
    }

    public string GetFullName(int memberId)
    {
        var m = _members.FirstOrDefault(m => m.Id == memberId);
        return m is not null ? $"{m.FirstName} {m.LastName}" : $"Member #{memberId}";
    }
}
