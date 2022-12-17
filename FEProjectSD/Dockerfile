FROM node:latest as node
WORKDIR /app
COPY package.json package-lock.json
COPY . .
RUN npm install -g npm@9.1.3
RUN npm install --legacy-peer-deps
RUN npm install -g @angular/cli

EXPOSE 4200
CMD ["ng", "serve", "--host=0.0.0.0"]
