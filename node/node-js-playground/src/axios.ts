import axios from "axios";

export async function makeGetRequest() {
  try {
    const res = await axios.get("https://jsonplaceholder.typicode.com/todos/1");
    console.info(res.data);
  } catch (err) {
    console.error(err);
  }
}

interface TodoBase {
  name: string;
  isCompleted: boolean;
}
interface Todo extends TodoBase {
  id: number;
}

export async function makePostWithPreconfiguredClient() {
  try {
    const httpClient = axios.create({
      baseURL: "http://localhost:3002",
    });
    httpClient.defaults.auth = { username: "user", password: "pass" };
    httpClient.interceptors.request.use(
      (request) => {
        console.log(`performing request to ${request.url}`);
        return request;
      },
      (err) => {
        console.error(err);
        return Promise.reject(err);
      }
    );
    httpClient.interceptors.response.use(
      (response) => {
        console.log(`Got a response with status ${response.status}`);
        return response;
      },
      (err) => {
        console.error(err);
        return Promise.reject(err);
      }
    );

    const result = await httpClient.post<Todo>("/todos", {
      name: "TODO1",
      isCompleted: false,
    });

    console.log(result.status);
    console.dir(result.data);
  } catch (err) {
    console.error(err);
  }
}
