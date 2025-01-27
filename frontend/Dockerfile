### STAGE 1: Build ###
FROM node:22.13.1-alpine AS build
WORKDIR /fin-analyzer
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build:prod

### STAGE 2: Run ###
FROM nginx:1.27.3-alpine AS final
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /fin-analyzer/dist/fin-analyzer /usr/share/nginx/html