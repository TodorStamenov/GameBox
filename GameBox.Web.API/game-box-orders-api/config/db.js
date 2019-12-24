const mongoose = require('mongoose');
const config = require('./config');

module.exports = mongoose
  .connect(`mongodb://${config.dockerHostIp}:${config.mongoPort}/game-box`, {
    useNewUrlParser: true,
    useUnifiedTopology: true
  });
