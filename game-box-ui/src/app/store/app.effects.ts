import { CategoriesEffects } from './categories/categories.effects';
import { CartEffects } from './cart/cart.effects';
import { WishlistEffects } from './wishlist/wishlist.effects';
import { OrdersEffects } from './orders/orders.effects';
import { UsersEffects } from './users/users.effects';
import { GamesEffects } from './games/games.effects';

export const appEffects = [
  CategoriesEffects,
  CartEffects,
  WishlistEffects,
  OrdersEffects,
  UsersEffects,
  GamesEffects
];
