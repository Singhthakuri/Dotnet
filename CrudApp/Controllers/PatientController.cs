
using CrudApp.Data;
using CrudApp.Dtos;
using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    public readonly AppdbContext _dbcontext;
    public PatientController(AppdbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    //getAllpatients

    [HttpGet]
    public async Task<IActionResult> GetAllPatient()
    {
        var PatientExist = await _dbcontext.Patients.Include(s => s.AddressField).ToListAsync();
        if(!PatientExist.Any())
        {
            return NotFound(new
            {
                message = "No patients Found"
            });
        }

        return Ok(new { message = "successfully fetched", data = PatientExist });
    }

    //Addpatients
    [HttpPost("AddPatient")]
    public async Task<IActionResult> PatientAdd([FromBody] AddPatientDtos PatientData)
    {
        if (PatientData == null)
        {
            return BadRequest(new { message = "All Field required" });
        }

        var NewPatient = new Patient
        {
            PatietName = PatientData.Name,
            patientDisease = PatientData.Disease,
            Age = Convert.ToInt32(PatientData.age),
            AddressField = PatientData.Addresses.Select(s => new Address
            {
                District = s.District,
                Street = s.Street,
                Tole = s.Tole
            }).ToList() ?? new List<Address>()

            
        };
         _dbcontext.Patients.Add(NewPatient);
        await _dbcontext.SaveChangesAsync();
        return Ok(
            new
            {
                message = "Patient Created",
                PatientList = NewPatient
            });
    }


    //Deletepatients
    [HttpDelete("Deletepatient/{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var patientExist = await _dbcontext.Patients.Include(s=>s.AddressField)
            .FirstOrDefaultAsync(s => s.Id == id);
        if(patientExist==null)
        {
            return NotFound(new { message = "Patient not found " });
        }

        _dbcontext.Patients.Remove(patientExist);
        await _dbcontext.SaveChangesAsync();
        return Ok(new
        {
            message = $"patient with {id} id deleted"
        });

    }


    //EditPatient
    [HttpPut("update/{id}/{id2}")]
    public async Task<IActionResult> EditTask(int id,int id2, [FromBody] AddPatientDtos patientData )
    {
        var PatientExist = await _dbcontext.Patients.Include(s => s.AddressField).FirstOrDefaultAsync(p => p.Id == id); ;
        if(PatientExist==null)
        {
            return NotFound(new
            {
                message = $"cannot found patient with this {id}"
            });
        }

        PatientExist.PatietName = patientData.Name;
        PatientExist.patientDisease = patientData.Disease;
        PatientExist.Age = Convert.ToInt32(patientData.age);


        var editAbleAddress = PatientExist.AddressField.FirstOrDefault(s=>s.Id==id2);
        var newAddresses = patientData.Addresses.FirstOrDefault();
        if(editAbleAddress!=null)
        {
            editAbleAddress.District = newAddresses.District;
            editAbleAddress.Street = newAddresses.Street;
            editAbleAddress.Tole = newAddresses.Tole;
        }

      
        await _dbcontext.SaveChangesAsync();
        return Ok(new
        {
            message = $"patient with  {id} has been Updated"
        });
      }

    //delete address
    [HttpDelete("deleteaddress/{id1}")]
    public async Task<IActionResult> deleteAddresses(int id1)
    {
        var address = await _dbcontext.AddressTable
            .FirstOrDefaultAsync(a=>a.Id==id1);
        if (address == null)
        {
            return NotFound(new { message = "Address not found" });
        }

        _dbcontext.AddressTable.Remove(address);
        await _dbcontext.SaveChangesAsync();

        return Ok(new {message="address Field Deleted"});
    }

}

