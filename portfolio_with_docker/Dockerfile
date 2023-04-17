# Use an official Node.js runtime as a parent image
FROM node:17-alpine

# Set the working directory to /app
WORKDIR /app

# Copy package.json and package-lock.json to the container
COPY package.json ./
COPY package-lock.json ./
# Build the application dependencies from package.json
RUN npm config set legacy-peer-deps true 

RUN npm ci

# Copy the rest of the app code to the container
COPY . .

# Build the app
RUN npm run build

EXPOSE 3030

# Set the command to start the app
CMD ["npm", "start"]