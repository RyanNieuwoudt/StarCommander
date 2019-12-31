import React, { useCallback } from "react";
import { Text } from "grommet";
import { useDispatch } from "react-redux";
import { Link } from "react-router-dom";
import { actionCreators } from "store/auth";

export default function SignOutLink() {
	const dispatch = useDispatch();

	const onClick = useCallback(() => {
		dispatch(actionCreators.signOut());
	}, [dispatch]);

	return (
		<Link onClick={onClick} to="#">
			<Text>Sign out</Text>
		</Link>
	);
}
