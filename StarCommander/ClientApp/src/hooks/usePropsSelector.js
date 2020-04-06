import { useEffect } from "react";
import { useSelector } from "react-redux";

export default (selector, props) => {
	const id = selector.idSelector && selector.idSelector(null, props);
	useEffect(() => selector.use && selector.use(id), [selector, id]);
	return useSelector((x) => selector(x, props));
};
