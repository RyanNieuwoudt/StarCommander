import * as React from "react";
import { Link } from "react-router-dom";
import { Box, Header, Text } from "grommet";
import { SignOutLink } from ".";

export default function NavMenu() {
	return (
		<Header background="light-4" pad="small">
			<Link to="/">
				<Text>StarCommander</Text>
			</Link>
			<Box direction="row" gap="medium">
				<Link to="/counter">
					<Text>Counter</Text>
				</Link>
				<Link to="/fetch-data">
					<Text>Fetch Data</Text>
				</Link>
				<SignOutLink />
			</Box>
		</Header>
	);
}
