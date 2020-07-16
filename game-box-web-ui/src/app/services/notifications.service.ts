import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import * as signalR from '@aspnet/signalr';

import { AuthService } from './auth.service';
import { IAppState } from 'src/app/store/app.state';
import { ToastType } from '../modules/core/enums/toast-type.enum';
import { DisplayToastMessage } from 'src/app/store/+store/core/core.actions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private categoriesUrl = environment.api.notificationsUrl;
  private hubConnection: signalR.HubConnection;

  constructor(
    private authService: AuthService,
    private store: Store<IAppState>
  ) { }

  public subscribeForNotifications(): void {
    const options = {
      accessTokenFactory: () => this.authService.user.token
    };

    this.hubConnection = new signalR
      .HubConnectionBuilder()
      .withUrl(this.categoriesUrl, options)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on('ReceiveNotification', data => {
      this.store.dispatch(new DisplayToastMessage({
        message: `${data.title} is now available in store!`,
        toastType: ToastType.success
      }));
    });
  }
}