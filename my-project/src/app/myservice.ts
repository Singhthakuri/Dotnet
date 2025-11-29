import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAddressList, IPatient } from '../addpatient/addpatient';
import { Observable } from 'rxjs';
import { NumberValueAccessor } from '@angular/forms';

interface IAddressResponse{
  district:string,
  street:string,
  tole:string,
  id:number
}


export interface IPatientResonses{
  id:number,
  patietName:string,
  patientDisease:string,
  age:string,
  addressField:IAddressResponse[]
}


export interface IResponse<T>{
  message:string,
  data:T
}

@Injectable({
  providedIn: 'root',
})
export class Myservice {
    constructor(private http:HttpClient){}
// addpatients
  addPatient(data:IPatient){
    return this.http.post("http://localhost:5080/api/patient/AddPatient",data);
  }
// getallpatients
  getpatients()
  {
     return this.http.get<IResponse<IPatientResonses[]>>("http://localhost:5080/api/patient");
  }
// update
  updatepatient(data:IPatient,id:number,id2:number){
    return this.http.put(`http://localhost:5080/api/patient/update/${id}/${id2}`,data)
  }
  // delete
  delete(id:number){
     return this.http.delete(`http://localhost:5080/api/patient/Deletepatient/${id}`);
  }
  //deleteByid
  deleteByid(id:number){
    return this.http.delete(`http://localhost:5080/api/patient/deleteaddress/${id}`);
  }
}
