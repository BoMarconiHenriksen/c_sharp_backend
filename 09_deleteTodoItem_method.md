# Add a DeleteTodoItem method and test it
Add the following DeleteTodoItem method:  
```
// DELETE: api/Todo/5
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteTodoItem(long id)
{
    var todoItem = await _context.TodoItems.FindAsync(id);

    if (todoItem == null)
    {
        return NotFound();
    }

    _context.TodoItems.Remove(todoItem);
    await _context.SaveChangesAsync();

    return NoContent();
}
```
The DeleteTodoItem response is 204 (No Content).  

### Test the DeleteTodoItem method
Use Postman to delete a to-do item:  

Set the method to DELETE.  
Set the URI of the object to delete, for example https://localhost:5001/api/todo/1   
Select Send  
The sample app allows you to delete all the items, but when the last item is deleted, a new one is created by the model class constructor the next time the API is called.  
