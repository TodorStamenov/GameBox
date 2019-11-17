const jwt = require('./jwt');

module.exports = function(requiredRole = '') {
  return function(req, res, next) {
    if (!req.headers.authorization
      || !req.headers.authorization.startsWith('Bearer ')) {
      res.status(401).send('[UNAUTHORIZED]');
      return;
    }

    const token = req.headers.authorization.split(' ')[1];

    jwt.verify(token)
      .then(result => {
        const tokenExpiration = result.exp * 1000;
        const role = result['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

        if (tokenExpiration < Date.now()
          || (requiredRole && role !== requiredRole)) {
          return Promise.reject();
        }

        next();
      })
      .catch(() => {
        res.status(401).send('[UNAUTHORIZED]');
      });
  }
};
