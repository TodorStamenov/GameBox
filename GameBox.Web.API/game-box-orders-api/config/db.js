const mongoose = require('mongoose');

module.exports = mongoose.connect(
  'mongodb://localhost:27017/game-box', {
    useNewUrlParser: true,
    useUnifiedTopology: true
  });
