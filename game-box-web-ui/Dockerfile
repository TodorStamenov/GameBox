FROM node:12.14.0-alpine as builder
EXPOSE 4242

WORKDIR /app

COPY game-box-web-ui .
RUN npm install
RUN npm run build

FROM nginx:1.17.1-alpine
COPY --from=builder /app/dist/game-box-web-ui /usr/share/nginx/html