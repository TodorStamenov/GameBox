// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

const baseHost = '172.17.0.1';

export const environment = {
  production: false,
  api: {
    gameBoxApiUrl: `http://${baseHost}:5000/api/`,
    notificationsUrl: `http://${baseHost}:5002/notifications`,
    ordersApiUrl: `http://${baseHost}:1337/api/`,
    graphQlUrl: `http://${baseHost}:5000/api/graphql`
  }
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
