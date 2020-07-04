FROM node:12.18.2-alpine as builder
EXPOSE 4242

WORKDIR /app

COPY game-box-web-ui/package.json .
COPY game-box-web-ui/package-lock.json .
RUN npm install

COPY game-box-web-ui .
RUN npm run build

FROM nginx:1.17.6-alpine
COPY --from=builder /app/dist/game-box-web-ui /usr/share/nginx/html