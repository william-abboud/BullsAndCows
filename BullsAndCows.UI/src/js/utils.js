const storage = window.localStorage;

export const getAccessToken = () => storage.getItem("access_token");

export const setAccessToken = token => storage.setItem("access_token", token);

export const removeAccessToken = () => storage.removeItem("access_token");

export const getUser = () => storage.getItem("user");

export const setUser = user => storage.setItem("user", user);

export const setUserId = id => storage.setItem("userId", id);

export const getUserId = () => storage.getItem("userId");

export const isLoggedIn = () => Boolean(getAccessToken());

export const request = (url, method = 'GET', body = {}) => (
  fetch(url, {
    "headers": {
      "Content-Type": "application/json",
    },
    method,
    body: method !== "GET" ? JSON.stringify(body) : {}
  })
);

export const authorizedRequest = (url, method = 'GET', body = {}) => {
  const token = getAccessToken();

  return fetch(url, {
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`,
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
  getUserId,
  setUserId,
  isLoggedIn,
  request,
  authorizedRequest,
};
