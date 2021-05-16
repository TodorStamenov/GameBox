"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AuthGuard = void 0;
const common_1 = require("@nestjs/common");
const jwt_1 = require("@nestjs/jwt");
const AuthGuard = (requiredRole) => {
    let AuthGuardMixing = class AuthGuardMixing {
        constructor(jwtService) {
            this.jwtService = jwtService;
        }
        canActivate(context) {
            var _a;
            const request = context.switchToHttp().getRequest();
            if (!((_a = request === null || request === void 0 ? void 0 : request.headers) === null || _a === void 0 ? void 0 : _a.authorization)
                || !request.headers.authorization.startsWith('Bearer ')) {
                throw new common_1.UnauthorizedException('[UNAUTHORIZED]');
            }
            const token = request.headers.authorization.split(' ')[1];
            const result = this.jwtService.verify(token);
            const tokenExpiration = result.exp * 1000;
            const role = result['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            if (tokenExpiration < Date.now()
                || (requiredRole && role.toLowerCase() !== requiredRole.toLowerCase())) {
                throw new common_1.UnauthorizedException('[UNAUTHORIZED]');
            }
            return true;
        }
    };
    AuthGuardMixing = __decorate([
        common_1.Injectable(),
        __metadata("design:paramtypes", [jwt_1.JwtService])
    ], AuthGuardMixing);
    return common_1.mixin(AuthGuardMixing);
};
exports.AuthGuard = AuthGuard;
//# sourceMappingURL=auth.guard.js.map