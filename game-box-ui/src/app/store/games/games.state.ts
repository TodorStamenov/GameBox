import { IGamesListModel } from 'src/app/modules/admin/models/games/games-list.model';
import { IGameBindingModel } from 'src/app/modules/admin/models/games/game-binding.model';

export interface IGamesState {
  all: IGamesListModel[];
  toEdit: IGameBindingModel;
}
