import { CommonModule, NgFor, NgIf } from '@angular/common';
import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IPatientResonses, IResponse, Myservice } from '../app/myservice';

export interface IAddressList{
  District:string,
  Street:string,
  Tole:string,
}

 export interface IPatient{
  Name:string,
  Disease:string,
  age:string | null,
  Addresses:IAddressList[]
}

@Component({
  selector: 'app-addpatient',
  standalone:true,
  imports: [CommonModule,FormsModule,NgFor,NgIf],
  templateUrl: './addpatient.html',
  styleUrl: './addpatient.css',
})
export class Addpatient implements OnInit {
addressEnable:boolean=false;
IsEditButton:boolean=false;
useIdForEdit:number | null=null;
useIdForAddressEdit:number | null=null;

patient:IPatient={
  Name:"",
  Disease:"",
  age:null,
  Addresses:[]
};
AllAddress:IAddressList={
  District:"",
  Street:"",
  Tole:""
};

Allpatients:IPatientResonses[]=[];

constructor(private http:Myservice){}

ngOnInit(): void {
  this.getpatients();
}

//make field nulls
makeFieldEmpty(){
  this.patient.Name="";
  this.patient.Disease="";
  this.patient.age="";
  this.AllAddress.District="";
  this.AllAddress.Street="";
  this.AllAddress.Tole="";
}

// multiles address Add 
addressAdder(){
  this.patient.Addresses=[
    ...this.patient.Addresses,
    {
      District:this.AllAddress.District,
      Street:this.AllAddress.Street,
      Tole:this.AllAddress.Tole
    }
    ]
  this.AllAddress.District="";
  this.AllAddress.Street="";
  this.AllAddress.Tole="";
}



toggleAddress(){
  this.addressEnable=!this.addressEnable;
}

// de;ete
delete(id:number){
  this.http.delete(id).subscribe({
    next:(res)=>{console.log("patient deleted",res),this.getpatients()},
    error:(err)=>{console.log("error occured",err)},
    complete:()=>{console.log("delete patient success")}
  })
}

deleteAddress(id1:number){
  this.http.deleteByid(id1).subscribe({
    next:(res)=>{console.log("Deleted successfully"),res,this.getpatients()},
    error:(err)=>{console.log("error occured"),err},
    complete:()=>{console.log("address delete request complete")}
  })
}
// cancel
cancel(){
  this.IsEditButton=false;
}

// addresss edit part
editAddress(id1: number, id2: number) {
  this.IsEditButton = true;
  this.useIdForEdit = id1;
  this.useIdForAddressEdit = id2;
  const patient = this.Allpatients.find(p => p.id === id1);
  if (!patient) return console.error('Patient not found');
  const address = patient.addressField.find(a => a.id === id2);
  if (!address) return console.error('Address not found');
  this.patient.Name = patient.patietName;
  this.patient.Disease = patient.patientDisease;
  this.patient.age = patient.age;
  this.AllAddress.District = address.district;
  this.AllAddress.Street = address.street;
  this.AllAddress.Tole = address.tole;
}

//update section
update(){
    if(!this.useIdForEdit || !this.useIdForAddressEdit)
    {
      console.log("Id not found")
      return ;
    }


    const patientEdit=this.Allpatients.find((p)=>p.id==this.useIdForEdit!);
    const addressIndex=patientEdit?.addressField.findIndex((e)=>e.id==this.useIdForAddressEdit);
    patientEdit!.addressField[addressIndex!].district = this.AllAddress.District;
  patientEdit!.addressField[addressIndex!].street = this.AllAddress.Street;
  patientEdit!.addressField[addressIndex!].tole = this.AllAddress.Tole;
    console.log(this.patient);
 this.http.updatepatient(this.patient,this.useIdForEdit!,this.useIdForAddressEdit!).subscribe({
    next:(res)=>{console.log("Patient Added",res),
      this.getpatients();
      this.makeFieldEmpty();
      this.IsEditButton=false;
    },
    error:(err)=>{console.log("error occured",err)},
    complete:()=>{console.log("Update patient request complete")}
  })
}

// clear Form
clearForm(){
this.makeFieldEmpty();
}

//submit section
submitForm(){
  console.log(this.patient);
  this.addressEnable=false;
  this.http.addPatient(this.patient).subscribe({
    next:(res)=>{
     console.log("patient added successfully",res);
     this.getpatients();
      
    },
    error:(err)=>{
      console.log("error occured",err);
    },
    complete:()=>{
      console.log("Add patient request  completed")
    }
  })
  
}
//get patients
getpatients(){
  this.http.getpatients().subscribe({
    next:(res:IResponse<IPatientResonses[]>)=>{
     this.Allpatients=res.data;
     console.log(this.Allpatients);
    
    },
    error:(err)=>{
      console.log("error occured",err);
    },
    complete:()=>{
      console.log("Get patients request  completed")
    }
  })
}

}
