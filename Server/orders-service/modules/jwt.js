const jwt = require('jsonwebtoken');

module.exports = {
  verify: function (token) {
    return new Promise((resolve, reject) => {
      jwt.verify(token, 'superSecretKey@345', function (err, data) {
        if (err) {
          reject(err);
          return;
        }

        resolve(data);
      });
    });
  }
};
