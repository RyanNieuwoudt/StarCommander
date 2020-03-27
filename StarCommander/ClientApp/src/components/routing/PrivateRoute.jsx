import React, { useCallback } from "react";
import { useSelector } from "react-redux";
import { Route } from "react-router-dom";
import { RedirectOnce } from ".";
import { isLoggedIn } from "selectors";

export default function PrivateRoute({ component: Component, ...rest }) {
	const loggedIn = useSelector(isLoggedIn);

	const render = useCallback(
		(props) =>
			Component && loggedIn ? (
				<Component {...props} />
			) : (
				<RedirectOnce to="/welcome" />
			),
		[Component, loggedIn]
	);

	return <Route {...rest} render={render} />;
}
