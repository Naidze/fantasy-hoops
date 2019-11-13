import decode from 'jwt-decode';

export const parse = () => {
  const token = localStorage.getItem('accessToken');
  try {
    const decoded = decode(token);
    if (decoded.exp > Date.now() / 1000) {
      return decoded;
    }

    localStorage.removeItem('accessToken');
    return null;
  } catch (err) {
    return null;
  }
};

export const isAuth = () => {
  const token = localStorage.getItem('accessToken');
  if (!token) { return false; }

  return parse();
};

export const isAdmin = isAuth() ? parse().isAdmin : false;
