import * as R from "ramda";
import React from "react";
import { Text } from "grommet";
import { useSelector } from "react-redux";
import { Player } from "store/auth";

export default function Name() {
	const { firstName, lastName } = useSelector(
		R.path(["auth", "player"])
	) as Player;

	return (
		<Text>
			{firstName} {lastName}
		</Text>
	);
}
