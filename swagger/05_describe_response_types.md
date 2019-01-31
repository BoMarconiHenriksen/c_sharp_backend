# Beskriv response typer
Beskriv http status kode.  
```
        // POST: api/Todo
        /// <summary>
        /// Create a new TodoItem if collection is empty
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     } 
        /// </remarks>
        /// <param name="context"></param>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item) {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
```



