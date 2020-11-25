import qs from "qs";

export default (token: string | undefined, config = {}) => {
	const Authorization = token ? "Bearer " + token : null;

	const headers = {
		"Content-Type": "application/json",
		...(Authorization && { Authorization }),
	};

	const paramsSerializer = (params: any) => {
		return qs.stringify(params);
	};

	return { headers, paramsSerializer, ...config };
};
