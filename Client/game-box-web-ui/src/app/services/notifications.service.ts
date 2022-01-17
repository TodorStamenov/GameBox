import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import * as signalR from '@microsoft/signalr';

import { AuthService } from './auth.service';
import { IAppState } from 'src/app/store/app.state';
import { ToastType } from '../modules/core/enums/toast-type.enum';
import { LoadAllGamesHome, ClearGamesHome } from '../modules/game/+store/games.actions';
import { DisplayToastMessage } from 'src/app/store/+store/core/core.actions';
import { LoadCategoryNames } from '../store/+store/category/categories.actions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private notificationsUrl = environment.api.notificationsUrl;
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
      .withUrl(this.notificationsUrl, options)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on('ReceiveNotification', data => {
      this.store.dispatch(new ClearGamesHome());
      this.store.dispatch(new LoadCategoryNames());
      this.store.dispatch(new LoadAllGamesHome(0));
      this.store.dispatch(new DisplayToastMessage({
        message: `${data.title} is now available in store!`,
        toastType: ToastType.success
      }));
    });
  }
}
