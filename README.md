# MyValueObject with fluent validation approach

## Working in repository

### Create Branch

1. Update the **master** and/or **dev** branches to latest version(s)
  - Always branch from the place it will be merged back into
2. Create branch 
  - name should be same as task number + short descriptive name
  - prefixed with initials (eg. `ivanov`)
**Example**
`ivanov/12345-get-mpp-working`

### Make Changes

3. Make change
  - Make sure all watch tasks are running (build, unit tests, component tests)
4. Test
  - Run full test suite locally
5. Commit with descriptive name

### Push changes

6. Pull master for any changes
7. Rebase branch onto master (nice practice to do it each day before development begins)
8. Push branch up to Azure DevOps Repo

### Pull Request

9. Open pull request
    - Title __must__ be started from __PBI number__
    - Longer descriptive title of task
    <br/>
**Example** <br/>
`#12211: Updated modals to accept Icons as component`
    - Bullet points with descriptions of changes (changelog) 
    - Tips: add PBI Id in commit (e.g. #12345), it will generate title and attach PBI in PR automatically.
10. CI server should run tests on pull request and update its status

### Code Review

11. Other devs should review code and leave notes
    - no confusing code
    - check for potential issues
    - any architectual improvements
    - code is properly abstracted
    - make sure there are appropriate tests

12. Code owner is responsible for making any necessary fixes and pushing them up
    - Follow same commit process
13. Other devs should give the code the thumbs up (at least two other devs)
    - Literally leave a comment with an emoji thumbs up (`:+1:` on github)
    - add short text of why you are okay with merging the pull request

### Merging Code

Once you have had enough people reviewed it, you are confident in the code you have written, and everything is well tested and passing.
You can start the deploy process.

14. Before merging PR **squash** commits 
    - Make sure all commit names make sense for their content (copy/paste task title if don't know how to call it)
    - Сommit names such as `fixed issue`, `fixed bugs`, `minor changes` **not allowed**
    - Сommit names should be prefixed with task number
**Example**
`[12345]: Updated modals to accept Icons as component`
15. Let the full test suite run again
16. Team Leads review PR at the end and their approve is final. After approve they complete PR
17. Merge the pull request.

## Repository method name convention

When a method taking a key as a parameter is prefixed with **Get**
` I except it to throw if it can't find anything with that key. `
```C#
    public async Task<T> GetBySpecificationAsync(
        ISpecification<T> spec,
        CancellationToken cancellationToken = default)
    {
        var intermediateResult = GetQueryableWithIncludesBySpecification(spec, _set);

        var dbItem = await intermediateResult.FirstOrDefaultAsync(spec.Criteria, cancellationToken);

        return dbItem ?? throw new ManpowerDomainException($"{typeof(T).Name} not found by specification.");
    }
```

When it's prefixed with **GetOrDefault** 
` I expect it to return **null** if no such item exists. `
```C#
    public async Task<T?> GetOrDefaultBySpecificationAsync(
        ISpecification<T> spec,
        CancellationToken cancellationToken = default)
    {
        var intermediateResult = GetQueryableWithIncludesBySpecification(spec, _set);
        
        return await intermediateResult.GetOrDefaultAsync(spec.Criteria, cancellationToken);
    }
```

## Executing sql procedures

In exceptional cases, we can add call with raw sql into RawSqlRepository.