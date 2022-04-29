import axios from "axios";

export async function makeGetRequest() {
  try {
    const res = await axios.get("https://jsonplaceholder.typicode.com/todos/1");
    console.info(res.data);
  } catch (err) {
    console.error(err);
  }
}
