import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { AppComponent } from './app.component';
import { SetPermissionComponent } from './components/user/set-permission/set-permission.component';
import { UserService } from './Services/User/userservise';

// не забываем декларировать добавленные компоненты и сервисы чтобы их видели остальные
@NgModule({
  declarations: [
    // сюда компоненты
    AppComponent, SetPermissionComponent
  ],
  imports: [
    BrowserModule, NgbModule.forRoot()
  ],
  providers: [
    // сюда сервисы
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
