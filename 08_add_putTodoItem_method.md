# Add a PutTodoItem method and test it
Add the following PutTodoItem method:  
```
// PUT: api/Todo/5
[HttpPut("{id}")]
public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
{
    if (id != item.Id)
    {
        return BadRequest();
    }

    _context.Entry(item).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
}
```
Update the to-do item that has id = 1 and set its name to "feed fish":  
```
{
    "ID":1,
    "name":"feed fish",
    "isComplete":true
}
```
