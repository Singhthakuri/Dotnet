import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Layout } from "../layout/layout";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Layout],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('my-project');
}
