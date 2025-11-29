import { Component } from '@angular/core';
import { Navbar } from "../navbar/navbar";
import { Addpatient } from "../addpatient/addpatient";
import { Footer } from "../footer/footer";

@Component({
  selector: 'app-layout',
  imports: [Navbar, Addpatient, Footer],
  templateUrl: './layout.html',
  styleUrl: './layout.css',
})
export class Layout {

}
