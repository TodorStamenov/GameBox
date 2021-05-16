"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppModule = void 0;
const jwt_1 = require("@nestjs/jwt");
const common_1 = require("@nestjs/common");
const mongoose_1 = require("@nestjs/mongoose");
const orders_controller_1 = require("./controllers/orders.controller");
const order_service_1 = require("./services/order.service");
const order_schema_1 = require("./models/order.schema");
const config_1 = require("./config/config");
let AppModule = class AppModule {
};
AppModule = __decorate([
    common_1.Module({
        imports: [
            jwt_1.JwtModule.register({ secret: 'superSecretKey@345' }),
            mongoose_1.MongooseModule.forRoot(`mongodb://${config_1.config.dockerHostIp}:${config_1.config.mongoPort}/game-box`, {
                useNewUrlParser: true,
                useUnifiedTopology: true
            }),
            mongoose_1.MongooseModule.forFeature([{ name: order_schema_1.Order.name, schema: order_schema_1.OrderSchema }])
        ],
        controllers: [orders_controller_1.OrdersController],
        providers: [order_service_1.OrderService]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map