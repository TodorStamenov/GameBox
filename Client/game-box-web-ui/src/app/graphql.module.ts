import { NgModule } from '@angular/core';

import { InMemoryCache } from '@apollo/client/core';
import { APOLLO_OPTIONS } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';

import { environment } from 'src/environments/environment';

@NgModule({
  providers: [{
    provide: APOLLO_OPTIONS,
    useFactory(httpLink: HttpLink) {
      return {
        cache: new InMemoryCache(),
        link: httpLink.create({
          uri: environment.api.graphQlUrl
        })
      };
    },
    deps: [HttpLink],
  }],
})
export class GraphQLModule { }
