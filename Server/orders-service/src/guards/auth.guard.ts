import {
  CanActivate,
  ExecutionContext,
  Injectable,
  mixin,
  UnauthorizedException,
} from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { Observable } from 'rxjs';

export const AuthGuard = (requiredRole: string): any => {
  @Injectable()
  class AuthGuardMixing implements CanActivate {
    constructor(private jwtService: JwtService) {}

    public canActivate(
      context: ExecutionContext,
    ): boolean | Promise<boolean> | Observable<boolean> {
      const request = context.switchToHttp().getRequest();
      if (
        !request?.headers?.authorization ||
        !request.headers.authorization.startsWith('Bearer ')
      ) {
        throw new UnauthorizedException('[UNAUTHORIZED]');
      }

      const token = request.headers.authorization.split(' ')[1];
      const result = this.jwtService.verify(token);

      const tokenExpiration = result.exp * 1000;
      const role =
        result['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

      if (
        tokenExpiration < Date.now() ||
        (requiredRole && role.toLowerCase() !== requiredRole.toLowerCase())
      ) {
        throw new UnauthorizedException('[UNAUTHORIZED]');
      }

      return true;
    }
  }

  return mixin(AuthGuardMixing);
};
