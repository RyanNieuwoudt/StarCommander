import * as R from "ramda";
import axios from "axios";
import { getConfig } from ".";

const resolve = (promise: Promise<any>) =>
	promise
		.then((response: { data: any }) => response.data)
		.catch((error: any) => {
			throw (
				R.path(["response", "data"], error) || {
					message: "System offline, please try again later.",
				}
			);
		});

export default {
	delete: (url: string, token: string) =>
		resolve(axios.delete(url, getConfig(token))),
	get: (url: string, token: string, config?: {} | undefined) =>
		resolve(axios.get(url, getConfig(token, config))),
	post: (url: string, data = {}, token: string | undefined = undefined) =>
		resolve(axios.post(url, data, getConfig(token))),
};
