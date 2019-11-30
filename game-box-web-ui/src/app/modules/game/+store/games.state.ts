import { IGamesListModel } from 'src/app/modules/game/models/games-list.model';
import { IGameBindingModel } from 'src/app/modules/game/models/game-binding.model';
import { IGamesHomeListModel } from 'src/app/modules/game/models/games-home-list.model';
import { IGameDetailsModel } from 'src/app/modules/game/models/game-details.model';
import { IAppState } from 'src/app/store/app.state';

export interface IState extends IAppState {
  games: IGamesState;
}

export interface IGamesState {
  all: IGamesListModel[];
  allHome: IGamesHomeListModel[];
  byCategory: IGamesHomeListModel[];
  owned: IGamesHomeListModel[];
  details: IGameDetailsModel;
  byId: IGameBindingModel;
}
