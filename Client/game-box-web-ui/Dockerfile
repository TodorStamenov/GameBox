FROM node:12.18.2-alpine as builder
EXPOSE 4200

WORKDIR /app

COPY package.json .
COPY package-lock.json .
RUN npm install

COPY . .
RUN npm run build

FROM nginx:1.19.1-alpine
COPY --from=builder /app/dist/game-box-web-ui /usr/share/nginx/html