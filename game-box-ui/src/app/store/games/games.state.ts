import { IGamesListModel } from 'src/app/modules/admin/models/games/games-list.model';
import { IGameBindingModel } from 'src/app/modules/admin/models/games/game-binding.model';
import { IGamesHomeListModel } from 'src/app/modules/user/models/games/games-list.model';
import { IGameDetailsModel } from 'src/app/modules/user/models/games/game-details.model';

export interface IGamesState {
  all: IGamesListModel[];
  byCategory: IGamesHomeListModel[];
  owned: IGamesHomeListModel[];
  detail: IGameDetailsModel;
  toEdit: IGameBindingModel;
}
