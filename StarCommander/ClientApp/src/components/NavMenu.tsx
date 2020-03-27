import * as React from "react";
import { Link } from "react-router-dom";
import { Box, Header, Text } from "grommet";
import { SignOutLink } from ".";
import { Name } from "components/player";

export default function NavMenu() {
	return (
		<Header background="light-4" pad="small">
			<Link to="/">
				<Text>Bridge</Text>
			</Link>
			<Link to="/fleet">
				<Text>Fleet</Text>
			</Link>
			<Name />
			<Box direction="row" gap="medium">
				<SignOutLink />
			</Box>
		</Header>
	);
}
