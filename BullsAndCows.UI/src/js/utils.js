export const getAccessToken = () => window.localStorage.getItem("access_token");
export const setAccessToken = (token) => window.localStorage.setItem("access_token", token);
export const getUser = () => window.localStorage.getItem("user");
export const setUser = (user) => window.localStorage.setItem("user", user);
export const isLoggedIn = () => Boolean(getAccessToken());
export const request = (url, method = 'GET', body = {}) => {
  return fetch(url, {
    "headers": {
      "Content-Type": "application/json",
    },
    method,
    body
  });
};
export const authorizedRequest = (url, method = 'GET', body = {}) => {
  const token = getAccessToken();

  return fetch(url, {
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer " + token,
    },
    method,
    body: method !== "GET" ? JSON.stringify(body) : {},
  });
};

export default {
  getAccessToken,
  setAccessToken,
  getUser,
  setUser,
  isLoggedIn,
  request,
  authorizedRequest,
};
