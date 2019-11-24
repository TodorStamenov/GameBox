const mongoose = require('mongoose');

module.exports = mongoose.connect(
  'mongodb://192.168.99.100:27018/game-box', {
    useNewUrlParser: true,
    useUnifiedTopology: true
  });
