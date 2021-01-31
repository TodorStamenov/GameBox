FROM node:12.7-alpine AS build

WORKDIR /usr/src/app

COPY package.json package-lock.json ./
RUN npm install

COPY . .
RUN npm run build

FROM nginx:1.17.1-alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build /usr/src/app/dist/game-box-web-ui .
COPY nginx.conf /etc/nginx/nginx.conf