# Test the PostTodoItem method
Build the project.  

In Postman, set the HTTP method to POST.  

Select the Body tab.  

Select the raw radio button.  

Set the type to JSON (application/json).  

In the request body enter JSON for a to-do item:  
```
{
  "name":"walk dog",
  "isComplete":true
}
```
If you get a 405 Method Not Allowed error, it's probably the result of not compiling the project after adding the PostTodoItem method.  

### Test the location header URI
Select the Headers tab in the Response pane.  

Copy the Location header value: example https://localhost:5001/api/todo/2  

Set the method to GET.  

Paste the URI (for example, https://localhost:5001/api/Todo/2)  

Select Send.  
