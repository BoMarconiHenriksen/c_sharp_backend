// PUT api/values/5
//[HttpPut("{id}")]
//public void Put(long id, [FromBody] Form form)
//{
//    // Get the row from the database
//    var formRow = _context.Form.Find(id);
//    //var changedHeadline = _context.FormField.Find(id);
//    // var formRow = _context.Form
//    //    .Include(form => form.FormFields)

//    if (formRow != null)
//    {
//        formRow.Name = form.Name;


//        _context.SaveChanges();
//    }
//}

// Another way
// PUT: api/Forms/5
[HttpPut("{id}")]
public async Task<IActionResult> PutForm(long id, Form form)
{

    if (id != form.Id)
    {
        return BadRequest();
    }


    // Edit Form table
    _context.Entry(form).State = EntityState.Modified;

    // Edit headline in FormFields
    foreach (var f in form.FormFields)
    {
        _context.Entry(f).State = EntityState.Modified;
    }

    // Edit headline in FormFields
    // form.FormFields.ToList().ForEach(f => _context.Entry(f).State = EntityState.Modified);

    // Edit headline in FormFields
    //var dbentity = _context.Form.Find(id);

    //if (dbentity != null)
    //{
    //    dbentity.Name = form.Name;

    //    foreach (var f in dbentity.FormFields)
    //    {
    //        var incomingFf = form.FormFields.SingleOrDefault(ef => ef.Id == f.Id);

    //        if (incomingFf != null)
    //        {
    //            f.Headline = incomingFf.Headline;
    //        }
    //    }
    //}

    try
    {
        await _context.SaveChangesAsync();

    }
    catch (DbUpdateConcurrencyException)
    {
        if (!FormExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return NoContent();
}
