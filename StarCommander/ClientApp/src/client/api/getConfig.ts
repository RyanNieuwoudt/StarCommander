import qs from "qs";

export default (token: string | undefined, config: object = {}) => {
	const Authorization = token ? "Bearer " + token : null;

	const headers = {
		"Content-Type": "application/json",
		...(Authorization && { Authorization })
	};

	const paramsSerializer = (params: object) => {
		return qs.stringify(params);
	};

	return { headers, paramsSerializer, ...config };
};
