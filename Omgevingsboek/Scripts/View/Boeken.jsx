var data = [
  { Naam: "Boek naam"},
  { Naam: "Boek"},
  { Naam: "Lololol"}
];

var CommentBox = React.createClass({
  render: function() {
    return (
        <div className="col-sm-6 col-md-3">
            <a href="#" style={{backgroundImage: "url('http://lorempixel.com/350/150/city/')"}}>
                <span className="caption">{this.props.naam}</span>
				<span className="caption">{this.props.afbeelding}</span>
            </a>
        </div>
    );
  }
});

var CommentList = React.createClass({
  render: function() {
      var commentNodes = [];
      for (var i = 0; i < this.props.data.length; i++) {
          commentNodes.push(<CommentBox afbeelding={this.props.images[i]} naam={this.props.data[i]} />);
      }
    });
    return (
		<div className="row books">
			<h3 className="rowheader">{this.props.rowHead}</h3>
			{commentNodes}
        </div>
    );
  }
});