FROM node:14.15.5-alpine AS build

WORKDIR /usr/src/app

COPY package.json package-lock.json ./
RUN npm install

COPY . .
RUN npm run build

FROM nginx:1.19.6-alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build /usr/src/app/dist/game-box-web-ui .
COPY nginx.conf /etc/nginx/nginx.conf